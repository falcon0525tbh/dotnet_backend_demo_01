<template>
  <div class="page">
    <div class="page-header">
      <h1>Projects</h1>
    </div>
    <div v-if="canCreate" class="create-row">
      <button class="btn primary" type="button" @click="showDialog = true">
        + Create Project
      </button>
    </div>
    <div class="table">
      <div class="row head" :class="{ 'has-actions': canCreate }">
        <div>Name</div>
        <div>Description</div>
        <div>Due Date</div>
        <div>Priority</div>
        <div>Active</div>
        <div>Members</div>
        <div v-if="canCreate"></div>
      </div>
      <div
        class="row"
        :class="{ 'has-actions': canCreate }"
        v-for="p in projects"
        :key="p.id"
      >
        <div class="cell strong">{{ p.name }}</div>
        <div class="cell ellipsis" :title="p.description">{{ p.description }}</div>
        <div class="cell mono">{{ fmtDate(p.dueDate) }}</div>
        <div class="cell">
          <span class="pill" :class="priorityClass(p.priority)">
            {{ priorityLabel(p.priority) }}
          </span>
        </div>
        <div class="cell">
          <span class="pill" :class="p.active ? 'pill-green' : 'pill-gray'">
            {{ p.active ? "Yes" : "No" }}
          </span>
        </div>
        <div class="cell ellipsis" :title="memberNames(p)">
          {{ memberNames(p) }}
        </div>
        <div class="cell actions" v-if="canCreate && canEditProject(p)">
          <button class="btn" type="button" @click="onEdit(p)">Edit</button>
        </div>
      </div>
    </div>
    <ProjectEditDialog
      v-if="showDialog"
      :users="users"
      :project="editingProject"
      @close="showDialog = false"
      @save="onSave"
    />
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import {
  fetchProjects,
  createProject,
  updateProject,
  fetchProjectById,
} from "../../api/projects";
import { priorityOptions, TaskPriority } from "../../enums/task";
import { useAuthStore } from "../../stores/auth";
import ProjectEditDialog from "../../components/project/ProjectEditDialog.vue";
import { Roles } from "../../enums/role";
import { fetchUsers } from "../../api/users";

const projects = ref([]);
const showDialog = ref(false);
const auth = useAuthStore();
const users = ref([]);
const editingProject = ref(null);
const fmtDate = (d) => d?.slice(0, 10) || "-";
const priorityLabel = (v) => priorityOptions.find((o) => o.value === v)?.label ?? v;
const priorityClass = (p) => {
  if (p === TaskPriority.HIGH) return "pill-red";
  if (p === TaskPriority.MEDIUM) return "pill-amber";
  if (p === TaskPriority.LOW) return "pill-green";
  return "pill-gray";
};
const memberNames = (p) =>
  (p.members && p.members.length
    ? p.members.map((m) => m.userName || m.name || m.username).join(", ")
    : "");
const userRole = computed(() =>
  auth.user?.role !== undefined && auth.user?.role !== null
    ? Number(auth.user.role)
    : null,
);
const userId = computed(() => auth.user?.id || null);
const isAdmin = computed(() => userRole.value === Roles.Admin);
const isManagerOrCeo = computed(() =>
  [Roles.Manager, Roles.CEO].includes(userRole.value),
);
const canCreate = computed(() =>
  [Roles.Admin, Roles.Manager, Roles.CEO].includes(userRole.value),
);
const canEditProject = (p) =>
  isAdmin.value || (isManagerOrCeo.value && p.createdBy === userId.value);

onMounted(async () => {
  const [projectsRes, usersRes] = await Promise.all([
    fetchProjects(),
    fetchUsers(),
  ]);
  projects.value = projectsRes.data;
  users.value = usersRes.data;
});

const onSave = async (payload) => {
  if (editingProject.value) {
    await updateProject(editingProject.value.id, payload);
    editingProject.value = null;
  } else {
    await createProject(payload);
  }
  const { data } = await fetchProjects();
  projects.value = data;
  showDialog.value = false;
};

const onEdit = (project) => {
  loadProjectDetail(project.id);
};

const loadProjectDetail = async (id) => {
  const { data } = await fetchProjectById(id);
  editingProject.value = {
    ...data,
    memberIds: data.memberIds || [],
    active: typeof data.active === "boolean" ? data.active : true,
  };
  showDialog.value = true;
};
</script>

<style scoped>
.page {
  max-width: 1200px;
  margin: 0 auto;
  padding: 24px;
}
.page-header {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  margin-bottom: 4px;
}
.create-row {
  margin: 8px 0 12px;
}
.table {
  display: grid;
  gap: 8px;
  background: #f8fafc;
  padding: 12px;
  border-radius: 12px;
  overflow-x: auto;
}
.row {
  display: grid;
  grid-template-columns: 1.6fr 2fr 1.2fr 1fr 0.8fr 1.4fr;
  gap: 8px;
  padding: 10px 12px;
  background: #fff;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.04);
}
.row.has-actions {
  grid-template-columns: 1.6fr 2fr 1.2fr 1fr 0.8fr 1.4fr 0.9fr;
}
.head {
  font-weight: 700;
  background: #e2e8f0;
  box-shadow: none;
}
.cell {
  font-size: 14px;
  color: #334155;
}
.cell.strong {
  font-weight: 600;
}
.cell.mono {
  font-family: ui-monospace, Menlo, monospace;
  font-size: 12px;
}
.cell.ellipsis {
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.pill {
  display: inline-block;
  padding: 2px 10px;
  border-radius: 999px;
  color: #fff;
  font-size: 12px;
  font-weight: 600;
}
.pill-green {
  background: #16a34a;
}
.pill-amber {
  background: #f59e0b;
}
.pill-red {
  background: #dc2626;
}
.pill-gray {
  background: #9ca3af;
}
.actions {
  display: flex;
}
</style>
