import { createRouter, createWebHistory } from 'vue-router'
import MainLayout from '../components/MainLayout.vue'
import { Roles } from '../enums/role'
import LoginView from '../views/Auth/LoginView.vue'

const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', name: 'login', component: LoginView },
  { path: '/',
    component: MainLayout,
    props: {
      menu: [
        { label: 'Dashboard', path: '/dashboard', roles: [Roles.Admin, Roles.CEO, Roles.Manager, Roles.Worker] },
        { label: 'Projects', path: '/projects', roles: [Roles.Admin, Roles.Manager, Roles.CEO, Roles.Worker] },
        { label: 'Task', path: '/task', roles: [Roles.Admin, Roles.CEO, Roles.Manager, Roles.Worker] },
        { label: 'Users', path: '/users', roles: [Roles.Admin] },
      ],
    },
    children: [
      { path: '', redirect: '/dashboard' },
      { path: 'dashboard', component: () => import('../views/Dashboard/Dashboard.vue') },
      { path: 'task', component: () => import('../views/Task/TaskView.vue') },
      { path: 'projects', component: () => import('../views/Project/ProjectListView.vue') },
      { path: 'users', component: () => import('../views/User/UserListView.vue') },
    ],
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
