export const TaskStatus = {
  NEW: 0,
  IN_PROGRESS: 1,
  UAT: 2,
  COMPLETED: 3,
  CLOSED: 4,
}

export const TaskPriority = {
  LOW: 0,
  MEDIUM: 1,
  HIGH: 2,
}

export const statusOptions = [
  { value: TaskStatus.NEW,         label: 'New' },
  { value: TaskStatus.IN_PROGRESS, label: 'In Progress' },
  { value: TaskStatus.UAT,         label: 'UAT' },
  { value: TaskStatus.COMPLETED,   label: 'Completed' },
  { value: TaskStatus.CLOSED,      label: 'Closed' },
]

export const priorityOptions = [
  { value: TaskPriority.LOW,    label: 'Low' },
  { value: TaskPriority.MEDIUM, label: 'Medium' },
  { value: TaskPriority.HIGH,   label: 'High' },
]