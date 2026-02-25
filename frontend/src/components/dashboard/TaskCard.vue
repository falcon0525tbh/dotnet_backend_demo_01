<template>
  <div class="table">
    <div class="row head">
      <div>{{ props.title }}</div>
      <div>Project</div>
      <div>Status</div>
      <div>Priority</div>
      <div>Due Date</div>
      <div>Assignee</div>
      <div>Creator</div>
      <div>Description</div>
    </div>

    <div class="row" v-for="task in tasks" :key="task.id">
      <div class="cell strong">{{ task.title }}</div>
      <div class="cell ellipsis" :title="task.projectDescription || ''">
        {{ task.projectName || "" }}
      </div>
      <div class="cell">
        <span class="pill" :class="statusClass(task.status)">
          {{ statusLabel(task.status) }}
        </span>
      </div>
      <div class="cell">
        <span class="pill" :class="priorityClass(task.priority)">
          {{ priorityLabel(task.priority) }}
        </span>
      </div>
      <div class="cell">{{ fmtDate(task.dueDate) }}</div>
      <div class="cell mono">{{ task.assigneeName }}</div>
      <div class="cell mono">{{ task.createdByName }}</div>
      <div class="cell ellipsis" :title="task.description">
        {{ task.description }}
      </div>
      <div class="cell">
        <button
          v-if="task.status !== TaskStatus.CLOSED"
          class="btn"
          @click="$emit('edit', task)"
        >
          Edit
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import {
  TaskPriority,
  TaskStatus,
  priorityOptions,
  statusOptions,
} from "../../enums/task";

const props = defineProps({
  title: { type: String, default: "Tasks" },
  tasks: { type: Array, default: () => [] },
});

const statusLabel = (v) => statusOptions.find((o) => o.value === v)?.label ?? v;
const priorityLabel = (v) =>
  priorityOptions.find((o) => o.value === v)?.label ?? v;
const statusClass = (s) => {
  if (s === TaskStatus.COMPLETED) return "pill-green";
  if (s === TaskStatus.CLOSED) return "pill-gray";
  if (s === TaskStatus.IN_PROGRESS) return "pill-blue";
  if (s === TaskStatus.NEW) return "pill-cyan";
  return "pill-gray";
};

const priorityClass = (p) => {
  if (p === TaskPriority.HIGH) return "pill-red";
  if (p === TaskPriority.MEDIUM) return "pill-amber";
  if (p === TaskPriority.LOW) return "pill-green";
  return "pill-gray";
};

const fmtDate = (d) => d?.slice(0, 10);
</script>

<style scoped>
.table {
  width: 100%;
  display: grid;
  grid-auto-rows: auto;
  gap: 8px;
  background: #f8fafc;
  padding: 12px;
  border-radius: 12px;
  overflow-x: auto; /* 小屏可横向滚动 */
}
.row {
  display: grid;
  grid-template-columns:
    minmax(180px, 1.5fr) /* Task */
    minmax(150px, 1fr) /* Project */
    minmax(120px, 1fr) /* Status */
    minmax(120px, 1fr) /* Priority */
    minmax(140px, 1fr) /* Due Date */
    minmax(160px, 1fr) /* Assignee */
    minmax(160px, 1fr) /* Creator */
    minmax(220px, 2fr) /* Description */
    minmax(100px, 0.8fr); /* Action */
  gap: 8px;
  padding: 10px 12px;
  background: #fff;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.04);
}
.head {
  font-weight: 700;
  color: #475569;
  box-shadow: none;
  background: #e2e8f0;
}
.cell {
  font-size: 14px;
  color: #334155;
}
.cell.strong {
  font-weight: 600;
}
.cell.mono {
  font-family: ui-monospace, SFMono-Regular, Menlo, monospace;
  font-size: 12px;
}
.cell.ellipsis {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
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
.pill-blue {
  background: #2563eb;
}
.pill-cyan {
  background: #0ea5e9;
}
.pill-gray {
  background: #9ca3af;
}
.pill-red {
  background: #dc2626;
}
.pill-amber {
  background: #f59e0b;
}
@media (max-width: 900px) {
  .row {
    grid-template-columns: repeat(3, minmax(160px, 1fr));
  }
}
</style>
