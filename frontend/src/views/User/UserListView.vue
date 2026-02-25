<template>
  <div class="page">
    <div class="page-header">
      <h1>Users</h1>
      <button class="btn primary" type="button" @click="onCreate">
        + Create User
      </button>
    </div>
    <div class="table">
      <div class="row head">
        <div>Id</div>
        <div>UserName</div>
        <div>PasswordHash</div>
        <div>Active</div>
        <div>CreatedAt</div>
        <div>Role</div>
        <div>Actions</div>
      </div>
      <div class="row" v-for="u in users" :key="u.id">
        <div class="cell mono">{{ u.id }}</div>
        <div class="cell">{{ u.userName }}</div>
        <div class="cell mono ellipsis" :title="u.passwordHash">
          {{ u.passwordHash }}
        </div>
        <div class="cell">
          <span :class="u.active ? 'pill-green' : 'pill-gray'">{{
            u.active ? "Yes" : "No"
          }}</span>
        </div>
        <div class="cell">{{ fmtDate(u.createdAt) }}</div>
        <div class="cell">{{ roleLabel(u.role) }}</div>
        <div class="cell actions">
          <button type="button" class="btn" @click="onEdit(u)">Edit</button>
        </div>
      </div>
    </div>
    <UserEditDialog
      v-if="showDialog"
      :user="editingUser"
      :roles="roleOptions"
      @close="onCloseDialog"
      @save="onSaveUser"
    />
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { fetchUsers, updateUser, createUser } from "../../api/users";
import { roleOptions } from "../../enums/role";
import UserEditDialog from "../../components/user/UserEditDialog.vue";

const users = ref([]);
const fmtDate = (d) => d?.slice(0, 10);
const roleLabel = (v) => roleOptions.find((o) => o.value === v)?.label ?? v;
const showDialog = ref(false);
const editingUser = ref(null);

const loadUsers = async () => {
  const { data } = await fetchUsers();
  users.value = data;
};

const onEdit = (user) => {
  editingUser.value = { ...user };
  showDialog.value = true;
};

const onCreate = () => {
  editingUser.value = {
    id: "",
    userName: "",
    password: "",
    role: roleOptions[0]?.value ?? null,
    active: true,
  };
  showDialog.value = true;
};

const onCloseDialog = () => {
  showDialog.value = false;
  editingUser.value = null;
};

const onSaveUser = async (payload) => {
  if (payload.id) {
    await updateUser(payload);
  } else {
    // create expects only UserName, Password, Role
    await createUser({
      userName: payload.userName,
      password: payload.password,
      role: payload.role,
    });
  }
  await loadUsers();
  onCloseDialog();
};

onMounted(async () => {
  await loadUsers();
});
</script>

<style scoped>
.page {
  max-width: 1200px;
  margin: 0 auto;
  padding: 24px;
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
  grid-template-columns: 1.8fr 1.4fr 2fr 0.8fr 1.2fr 1fr 1fr;
  gap: 8px;
  padding: 10px 12px;
  background: #fff;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.04);
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
.actions {
  display: flex;
  justify-content: flex-start;
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
.pill-green {
  background: #16a34a;
  color: #fff;
  padding: 2px 10px;
  border-radius: 999px;
  font-size: 12px;
}
.pill-gray {
  background: #9ca3af;
  color: #fff;
  padding: 2px 10px;
  border-radius: 999px;
  font-size: 12px;
}
</style>
