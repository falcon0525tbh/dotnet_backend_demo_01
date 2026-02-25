<template>
  <div class="mask" @click.self="$emit('close')">
    <div class="dialog">
      <h2>Edit Task</h2>
      <form class="form" @submit.prevent="saveTask">
        <div class="form-group">
          <label for="title">Title</label>
          <input id="title" v-model="form.title" required />
        </div>

        <div class="form-group">
          <label for="description">Description</label>
          <textarea id="description" v-model="form.description"></textarea>
        </div>

        <div class="form-group">
          <label>Status</label>
          <select v-model.number="form.status">
            <option v-for="o in statusOptions" :key="o.value" :value="o.value">
              {{ o.label }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label>Priority</label>
          <select v-model.number="form.priority">
            <option
              v-for="o in priorityOptions"
              :key="o.value"
              :value="o.value"
            >
              {{ o.label }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label>Due Date</label>
          <input type="date" v-model="form.dueDate" :min="tomorrow" />
        </div>

        <div class="form-group">
          <label>Assignee</label>
          <select v-model="form.assigneeId">
            <option disabled value="">Select Assignee</option>
            <option v-for="u in assignees" :key="u.id" :value="u.id">
              {{ u.userName || u.name || u.username }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label>Project (optional)</label>
          <select v-model="form.projectId">
            <option value="">No Project</option>
            <option v-for="p in projects" :key="p.id" :value="p.id">
              {{ p.name }}
            </option>
          </select>
        </div>

        <div class="form-group" v-if="form.id">
          <label>Creator</label>
          <input v-model="form.creatorName" readonly />
        </div>

        <div class="actions">
          <button type="button" @click="$emit('close')">Cancel</button>
          <button type="submit">
            {{ form.id ? "Save" : "Create" }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { computed, reactive, watch } from "vue";
import { priorityOptions, statusOptions } from "../../enums/task";

const props = defineProps({
  task: { type: Object, default: null },
  assignees: { type: Array, default: () => [] },
  projects: { type: Array, default: () => [] },
});
const emit = defineEmits(["close", "save"]);

const today = computed(() => new Date().toISOString().slice(0, 10));
const tomorrow = computed(() => {
  const d = new Date();
  d.setDate(d.getDate() + 1);
  return d.toISOString().slice(0, 10);
});
const form = reactive({
  id: "",
  title: "",
  description: "",
  id: "",
  status: 0,
  priority: 0,
  dueDate: tomorrow.value,
  assigneeId: "",
  assigneeName: "",
  projectId: null,
  creatorName: "",
});

watch(
  () => props.task,
  (t) => {
    Object.assign(form, {
      id: t?.id,
      title: t?.title || "",
      description: t?.description || "",
      status: t?.status ?? 0,
      priority: t?.priority ?? 0,
      dueDate:
        t?.dueDate?.slice(0, 10) || (t?.id ? today.value : tomorrow.value),
      assigneeId: t?.assigneeId || "",
      assigneeName: t?.assigneeName || "",
      projectId: t?.projectId ?? null,
      creatorName: t?.createdByName || "",
      id: t?.id || "",
    });
  },
  { immediate: true },
);

// 当选择新指派人时，同步显示姓名
watch(
  () => form.assigneeId,
  (id) => {
    const u = props.assignees.find((x) => x.id === id);
    form.assigneeName = u?.userName || u?.name || u?.username || "";
  },
);
const saveTask = () => emit("save", { ...form });
</script>

<style scoped>
.mask {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 20px;
}

.dialog {
  width: 420px;
  max-width: 90vw;
  background: #fff;
  border-radius: 12px;
  padding: 20px;
  box-shadow: 0 18px 40px rgba(0, 0, 0, 0.18);
}
.form {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 6px;
  text-align: left;
}

.form-group input,
.form-group select,
.form-group textarea {
  padding: 8px 10px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 14px;
}
.actions {
  margin-top: 12px;
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}
.actions button {
  padding: 8px 12px;
  border-radius: 8px;
  border: 1px solid #2563eb;
  background: #2563eb;
  color: #fff;
  cursor: pointer;
}
.actions button[type="button"] {
  background: #fff;
  color: #2563eb;
}
</style>
