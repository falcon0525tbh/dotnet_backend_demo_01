import http from './http'
export const fetchUsers = () => http.get('/User/users')
export const updateUser = (payload) => http.patch('/User/update', payload)
export const createUser = (payload) => http.post('/User/create', payload)
