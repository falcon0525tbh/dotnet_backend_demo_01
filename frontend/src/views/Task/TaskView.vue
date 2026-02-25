<template>
  <div class="dashboard">
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
      <div class="create-row">
        <button class="btn primary" @click="onCreateTask">+ Create Task</button>
      </div>
    </div>

    <!-- Tasks list -->
    <div class="grid">
      <div>
        <TaskCard
          :title="'All Tasks'"
          :tasks="tasks.allTasksList"
          @edit="onEditTask"
          empty="No Tasks"
        />
        <TaskCard
          :title="'Completed Tasks'"
          :tasks="tasks.completedTasksList"
          @edit="onEditTask"
          empty="No Completed Tasks"
        />
        <TaskCard
          :title="'Pending Tasks'"
          :tasks="tasks.pendingTasksList"
          @edit="onEditTask"
          empty="No Pending Tasks"
        />
        <TaskEditDialog
          v-if="showDialog"
          :task="editingTask"
          :assignees="users"
          :projects="projects"
          @close="onCloseDialog"
          @save="onSaveTask"
        />
      </div>
    </div>

    <div v-if="loading" class="banner">Loading tasks...</div>
    <div v-else-if="error" class="banner error">{{ error }}</div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { createTask, fetchDashboardTasks, updateTask } from "../../api/tasks";
import { fetchUsers } from "../../api/users";
import { fetchProjects } from "../../api/projects";
import StatCard from "../../components/dashboard/StatCard.vue";
import TaskCard from "../../components/dashboard/TaskCard.vue";
import TaskEditDialog from "../../components/dashboard/TaskEditDialog.vue";

const tasks = ref({
  totalTasks: 0,
  completedTasks: 0,
  pendingTasks: 0,
  allTasksList: [],
  completedTasksList: [],
  pendingTasksList: [],
});
const loading = ref(false);
const error = ref(null);
const showDialog = ref(false);
const editingTask = ref(null);
const users = ref([]);
const projects = ref([]);

onMounted(async () => {
  loading.value = true;
  try {
    const [tasksRes, usersRes, projectsRes] = await Promise.all([
      fetchDashboardTasks(),
      fetchUsers(),
      fetchProjects(),
    ]);
    tasks.value = tasksRes.data;
    users.value = usersRes.data;
    projects.value = projectsRes.data;
  } catch (err) {
    error.value = "Failed to load tasks.";
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
    const payload = {
      ...updatedTask,
      projectId: updatedTask.projectId || null,
    };
    if (updatedTask.id) {
      await updateTask(updatedTask.id, payload);
    } else {
      await createTask(payload);
    }
    onCloseDialog();
    onReloadTasks();
  } catch (err) {
    alert("Failed to save task.");
  }
};
const onReloadTasks = async () => {
  loading.value = true;
  error.value = null;
  try {
    const [tasksRes, projectsRes] = await Promise.all([
      fetchDashboardTasks(),
      fetchProjects(),
    ]);
    tasks.value = tasksRes.data;
    projects.value = projectsRes.data;
  } catch (err) {
    error.value = "Failed to reload tasks.";
  } finally {
    loading.value = false;
  }
};

const onCreateTask = () => {
  editingTask.value = {
    id: "",
    title: "",
    description: "",
    status: 0,
    priority: 0,
    dueDate: new Date().toISOString().slice(0, 10),
    assigneeId: "",
    assigneeName: "",
    projectId: "",
    createdByName: "",
  };
  showDialog.value = true;
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
