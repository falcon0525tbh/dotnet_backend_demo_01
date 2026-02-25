<template>
  <div class="mask" @click.self="$emit('close')">
    <div class="dialog">
      <h2>Edit User</h2>
      <form class="form" @submit.prevent="saveUser">
        <div class="form-group">
          <label>User Name</label>
          <input v-model="form.userName" required />
        </div>

        <div class="form-group">
          <label>Password</label>
          <input
            type="password"
            v-model="form.password"
            placeholder="Leave blank to keep current"
          />
        </div>

        <div class="form-group">
          <label>Role</label>
          <select v-model.number="form.role">
            <option v-for="o in roles" :key="o.value" :value="o.value">
              {{ o.label }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label>Active</label>
          <label class="switch">
            <input type="checkbox" v-model="form.active" />
            <span>{{ form.active ? "Enabled" : "Disabled" }}</span>
          </label>
        </div>

        <div class="actions">
          <button type="button" @click="$emit('close')">Cancel</button>
          <button type="submit">Save</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { reactive, watch } from "vue";

const props = defineProps({
  user: { type: Object, default: null },
  roles: { type: Array, default: () => [] },
});
const emit = defineEmits(["close", "save"]);

const form = reactive({
  id: "",
  userName: "",
  password: "",
  role: null,
  active: true,
});

watch(
  () => props.user,
  (u) => {
    Object.assign(form, {
      id: u?.id || "",
      userName: u?.userName || "",
      password: "",
      role: u?.role ?? props.roles[0]?.value ?? null,
      active: u?.active ?? true,
    });
  },
  { immediate: true },
);

const saveUser = () => {
  const payload = {
    id: form.id,
    userName: form.userName,
    role: form.role,
    active: form.active,
  };
  if (form.password) payload.password = form.password;
  emit("save", payload);
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
  font-size: 14px;
}
.form-group input,
.form-group select {
  padding: 8px 10px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 14px;
}
.switch {
  display: inline-flex;
  align-items: center;
  gap: 8px;
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
