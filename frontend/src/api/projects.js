import http from './http'

export const fetchProjects = () => http.get('/Projects')
export const createProject = (payload) => http.post('/Projects/create', payload)
export const updateProject = (id, payload) => http.patch(`/Projects/update/${id}`, payload)
export const fetchProjectById = (id) => http.get(`/Projects/${id}`)
