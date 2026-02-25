<template>
  <div class="card" :style="{ borderLeftColor: color }">
    <p class="label">{{ title }}</p>
    <p class="value" :style="{ color }">{{ value }}</p>
  </div>
</template>

<script setup>
defineProps({
  title: String,
  value: [Number, String],
  color: { type: String, default: "#2563eb" },
});
</script>

<style scoped>
.card {
  border: 2px solid #e5e7eb;
  border-left-width: 6px;
  border-radius: 12px;
  padding: 14px 16px;
  background: #fff;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.05);
}
.label {
  margin: 0;
  font-size: 14px;
  color: #6b7280;
}
.value {
  margin: 4px 0 0;
  font-size: 28px;
  font-weight: 700;
}
</style>
