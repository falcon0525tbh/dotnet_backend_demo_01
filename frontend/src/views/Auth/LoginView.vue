<template>
  <div class="login-page">
    <div class="login-container">
      <h1>Login</h1>
      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <label for="username">Username</label>
          <input type="text" id="userName" v-model="userName" required />
          <label for="password">Password</label>
          <input type="password" id="password" v-model="password" required />
          <button type="submit" :disabled="loading">
            {{ loading ? "Logging in..." : "Login" }}
          </button>
          <!-- Display error message if login fails -->
          <div v-if="error" class="error-message">{{ error }}</div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "../../stores/auth";

const router = useRouter();
const authStore = useAuthStore();

const userName = ref("");
const password = ref("");
const loading = ref(false);
const error = ref("");

const handleLogin = async () => {
  loading.value = true;
  error.value = "";
  try {
    await authStore.login(userName.value, password.value);
    router.push("/dashboard");
  } catch (err) {
    error.value = "Invalid username or password.";
  } finally {
    loading.value = false;
  }
};
</script>
