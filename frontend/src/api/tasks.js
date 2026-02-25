import http from './http'

export const fetchDashboardTasks = () => http.get('/Tasks/dashboard')
export const fetchDashboardTop = () => http.get('/Tasks/dashboard/top')
export const updateTask = (id, payload) => http.patch(`/Tasks/update/${id}`, payload)
export const createTask = (payload) => http.post('/Tasks/create', payload)
