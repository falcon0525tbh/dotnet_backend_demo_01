<template>
  <div class="dashboard">
    <div class="topbar">
      <div>
        <h1>Dashboard</h1>
        <p>
          Welcome to your dashboard! Here you can see an overview of your tasks
          and activities.
        </p>
      </div>
    </div>
    <div class="stats-bar">
      <div class="stats">
        <StatCard
          title="Total Tasks"
          :value="tasks.totalTasks"
          color="#0000FF"
        />
        <StatCard
          title="Completed Tasks"
          :value="tasks.completedTasks"
          color="#00FF00"
        />
        <StatCard
          title="Pending Tasks"
          :value="tasks.pendingTasks"
          color="#FF0000"
        />
      </div>
    </div>

    <!-- Tasks list -->
    <div class="grid">
      <div>
        <TaskCard
          title="Top Tasks"
          :tasks="topTasks"
          @edit="onEditTask"
          empty="No Tasks"
        />
      </div>
    </div>

    <div v-if="loading" class="banner">Loading dashboard data...</div>
    <div v-else-if="error" class="banner error">{{ error }}</div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import {
  createTask,
  fetchDashboardTasks,
  fetchDashboardTop,
  updateTask,
} from "../../api/tasks";
import { fetchUsers } from "../../api/users";
import StatCard from "../../components/dashboard/StatCard.vue";
import TaskCard from "../../components/dashboard/TaskCard.vue";
import TaskEditDialog from "../../components/dashboard/TaskEditDialog.vue";

const tasks = ref({
  totalTasks: 0,
  completedTasks: 0,
  pendingTasks: 0,
});
const topTasks = ref([]);
const loading = ref(false);
const error = ref(null);
const showDialog = ref(false);
const editingTask = ref(null);
const users = ref([]);

onMounted(async () => {
  loading.value = true;
  try {
    const [tasksRes, usersRes, topRes] = await Promise.all([
      fetchDashboardTasks(),
      fetchUsers(),
      fetchDashboardTop(),
    ]);
    tasks.value = tasksRes.data;
    users.value = usersRes.data;
    topTasks.value = topRes.data;
  } catch (err) {
    error.value = "Failed to load dashboard tasks.";
  } finally {
    loading.value = false;
  }
});

const onEditTask = (task) => {
  editingTask.value = { ...task };
  showDialog.value = true;
};
const onCloseDialog = () => {
  showDialog.value = false;
  editingTask.value = null;
};
const onSaveTask = async (updatedTask) => {
  try {
    if (updatedTask.id) {
      await updateTask(updatedTask.id, updatedTask);
    } else {
      await createTask(updatedTask);
    }
    onCloseDialog();
    onReloadDashboard();
  } catch (err) {
    alert("Failed to save task.");
  }
};
const onReloadDashboard = async () => {
  loading.value = true;
  error.value = null;
  try {
    const [tasksRes, topRes] = await Promise.all([
      fetchDashboardTasks(),
      fetchDashboardTop(),
    ]);
    tasks.value = tasksRes.data;
    topTasks.value = topRes.data;
  } catch (err) {
    error.value = "Failed to reload dashboard tasks.";
  } finally {
    loading.value = false;
  }
};

</script>

<style scoped>
.dashboard {
  max-width: 1200px;
  margin: 0 auto;
  padding: 24px;
}
.stats {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  gap: 16px;
  margin-bottom: 20px;
}
.create-row {
  margin: 8px 0 12px;
}
.grid {
  display: grid;
  gap: 16px;
}
.side {
  display: grid;
  gap: 16px;
}
.banner {
  margin-top: 16px;
  padding: 12px;
  background: #eef2ff;
  border-radius: 8px;
}
.banner.error {
  background: #fef2f2;
  color: #b91c1c;
}
@media (max-width: 900px) {
  .grid {
    grid-template-columns: 1fr;
  }
}
</style>
