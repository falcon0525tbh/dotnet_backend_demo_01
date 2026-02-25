<template>
  <div class="layout">
    <aside class="sidebar">
      <div class="brand">TaskHub</div>
      <nav class="menu">
        <RouterLink
          v-for="item in visibleMenu"
          :key="item.path"
          :to="item.path"
          class="nav-item"
          active-class="active"
        >
          {{ item.label }}
        </RouterLink>
      </nav>
      <button class="logout" type="button" @click="onLogout">Logout</button>
    </aside>
    <div class="main">
      <header class="topbar">
        <slot name="header"></slot>
      </header>
      <section class="content">
        <RouterView />
      </section>
    </div>
  </div>
</template>

<script setup>
import { computed } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "../stores/auth";

const props = defineProps({
  menu: { type: Array, default: () => [] }, // [{label, path, roles:['Admin',...], icon:'📊'}]
});

const auth = useAuthStore();
const router = useRouter();
const userRole = computed(() =>
  auth.user?.role !== undefined ? Number(auth.user.role) : null,
);
const visibleMenu = computed(() =>
  props.menu.filter(
    (m) =>
      !m.roles || userRole.value === null || m.roles.includes(userRole.value),
  ),
);

const onLogout = () => {
  auth.logout();
  router.push("/login");
};
</script>

<style scoped>
.layout {
  display: grid;
  grid-template-columns: 220px 1fr;
  min-height: 100dvh;
  background: #f5f7fb;
}

.sidebar {
  background: #0f172a;
  color: #e2e8f0;
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 12px;
}
.menu {
  display: flex;
  flex-direction: column;
  gap: 8px;
  flex: 1;
}
.brand {
  font-weight: 700;
  font-size: 18px;
  margin-bottom: 12px;
}
.nav-item {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 10px 12px;
  border-radius: 10px;
  color: inherit;
  text-decoration: none;
}
.nav-item:hover,
.nav-item.active {
  background: #1f2937;
}
.logout {
  margin-top: auto;
  padding: 10px 12px;
  border-radius: 10px;
  border: 1px solid #1f2937;
  background: #111827;
  color: #e2e8f0;
  cursor: pointer;
  transition:
    background 0.2s ease,
    transform 0.1s ease;
}
.logout:hover {
  background: #1f2937;
}
.logout:active {
  transform: translateY(1px);
}
.icon {
  width: 18px;
  text-align: center;
}

.main {
  display: flex;
  flex-direction: column;
  min-width: 0;
}
.topbar {
  background: #fff;
  border-bottom: 1px solid #e5e7eb;
}
.content {
  padding: 20px;
  overflow: auto;
}
</style>
