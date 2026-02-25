<template>
  <div class="mask" @click.self="$emit('close')">
    <div class="dialog">
      <h2>{{ form.id ? "Edit Project" : "Create Project" }}</h2>
      <form class="form" @submit.prevent="saveProject">
        <div class="form-group">
          <label>Name</label>
          <input v-model="form.name" required />
        </div>

        <div class="form-group">
          <label>Description</label>
          <textarea v-model="form.description" rows="3" />
        </div>

        <div class="form-group">
          <label>Due Date</label>
          <input type="date" v-model="form.dueDate" />
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
          <label>Status</label>
          <select v-model="form.active">
            <option :value="true">Active</option>
            <option :value="false">Inactive</option>
          </select>
        </div>

        <div class="form-group">
          <label>Members</label>
          <div class="chip-box" @click="toggleDropdown">
            <template v-if="selectedUsers.length">
              <span v-for="u in selectedUsers" :key="u.id" class="chip">
                {{ displayName(u) }}
                <button
                  type="button"
                  class="chip-close"
                  @click.stop="removeUser(u.id)"
                >
                  ×
                </button>
              </span>
            </template>
            <span v-else class="placeholder">Select members...</span>
            <span class="caret" aria-hidden="true"></span>
          </div>
          <div v-if="dropdownOpen" class="dropdown">
            <div
              class="option"
              v-for="u in availableUsers"
              :key="u.id"
              :class="{ active: form.memberIds.includes(u.id) }"
              @click="toggleUser(u.id)"
            >
              {{ displayName(u) }}
            </div>
          </div>
        </div>

        <div class="actions">
          <button type="button" @click="$emit('close')">Cancel</button>
          <button type="submit">Create</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import {
  computed,
  onBeforeUnmount,
  onMounted,
  reactive,
  ref,
  watch,
} from "vue";
import { priorityOptions, TaskPriority } from "../../enums/task";

const props = defineProps({
  users: { type: Array, default: () => [] },
  project: { type: Object, default: null },
});
const emit = defineEmits(["close", "save"]);

const form = reactive({
  id: null,
  name: "",
  description: "",
  dueDate: "",
  priority: TaskPriority.MEDIUM,
  memberIds: [],
  active: true,
});

const dropdownOpen = ref(false);
const displayName = (u) => u.userName || u.name || u.username || "Unknown";

const mergedUsers = computed(() => {
  const list = [...props.users];
  if (props.project?.members?.length) {
    props.project.members.forEach((m) => {
      if (!list.some((u) => u.id === m.id)) list.push(m);
    });
  }
  return list;
});

const selectedUsers = computed(() =>
  mergedUsers.value.filter((u) => form.memberIds.includes(u.id)),
);
const availableUsers = computed(() =>
  mergedUsers.value.filter((u) => !form.memberIds.includes(u.id)),
);

watch(
  () => props.project,
  (p) => {
    if (!p) {
      Object.assign(form, {
        id: null,
        name: "",
        description: "",
        dueDate: "",
        priority: TaskPriority.MEDIUM,
        memberIds: [],
      });
      return;
    }
    Object.assign(form, {
      id: p.id || null,
      name: p.name || "",
      description: p.description || "",
      dueDate: p.dueDate ? p.dueDate.slice(0, 10) : "",
      priority: p.priority ?? TaskPriority.MEDIUM,
      memberIds: Array.isArray(p.memberIds) ? [...p.memberIds] : [],
      active: typeof p.active === "boolean" ? p.active : true,
    });
  },
  { immediate: true },
);

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const toggleUser = (id) => {
  const idx = form.memberIds.indexOf(id);
  if (idx >= 0) {
    form.memberIds.splice(idx, 1);
  } else {
    form.memberIds.push(id);
  }
};

const removeUser = (id) => {
  const idx = form.memberIds.indexOf(id);
  if (idx >= 0) form.memberIds.splice(idx, 1);
};

const handleClickOutside = (e) => {
  if (!dropdownOpen.value) return;
  const insideDialog = e.target.closest(".dialog");
  const inDropdown = e.target.closest(".dropdown");
  const inChipBox = e.target.closest(".chip-box");
  if (insideDialog && !inDropdown && !inChipBox) {
    dropdownOpen.value = false;
  }
};

onMounted(() => document.addEventListener("click", handleClickOutside));
onBeforeUnmount(() =>
  document.removeEventListener("click", handleClickOutside),
);

const saveProject = () => {
  emit("save", { ...form });
};
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
  z-index: 1000;
}
.dialog {
  width: 440px;
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
  font-size: 14px;
}
.form-group input,
.form-group select,
.form-group textarea {
  padding: 8px 10px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 14px;
}
.form-group select {
  appearance: none;
  background: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='8' viewBox='0 0 12 8'%3E%3Cpath fill='%2364748b' d='M1 1l5 5 5-5'/%3E%3C/svg%3E")
    no-repeat right 12px center;
  padding-right: 36px;
}
.chip-box {
  min-height: 42px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  padding: 6px 10px;
  display: flex;
  flex-wrap: wrap;
  gap: 6px;
  align-items: center;
  cursor: pointer;
  position: relative;
}
.chip {
  background: #e2e8f0;
  color: #0f172a;
  padding: 4px 8px;
  border-radius: 8px;
  border: 1px solid #cbd5e1;
  display: inline-flex;
  align-items: center;
  gap: 6px;
  font-size: 13px;
}
.chip-close {
  border: none;
  background: transparent;
  color: #0f172a;
  cursor: pointer;
  font-size: 14px;
  line-height: 1;
}
.placeholder {
  color: #94a3b8;
  font-size: 14px;
}
.caret {
  margin-left: auto;
  width: 12px;
  height: 8px;
  background: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='8' viewBox='0 0 12 8'%3E%3Cpath fill='%2364748b' d='M1 1l5 5 5-5'/%3E%3C/svg%3E")
    no-repeat center center;
}
.dropdown {
  margin-top: 4px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  max-height: 220px;
  overflow: auto;
  background: #fff;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
}
.option {
  padding: 8px 12px;
  cursor: pointer;
}
.option:hover {
  background: #f1f5f9;
}
.option.active {
  background: #e0f2fe;
  color: #0f172a;
  font-weight: 600;
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
