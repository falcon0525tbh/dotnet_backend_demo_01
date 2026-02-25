import { defineStore } from 'pinia'
import { login as loginApi } from '../api/auth'

export const useAuthStore = defineStore('auth', {
    state:() => ({
        token: localStorage.getItem('token') || '', 
        user: localStorage.getItem('role')? { id: localStorage.getItem('userId'), role: localStorage.getItem('role') }: null,
     }),
    actions: {
        async login(userName, password){
            const {data} = await loginApi(userName, password)
            this.token = data.token
            this.user = {id:data.userId, role:data.role}
            localStorage.setItem('token', data.token)
            localStorage.setItem('userId', data.userId)
            localStorage.setItem('role', data.role)
        },
        logout() {
            this.token = ''
            this.user = null
            localStorage.removeItem('token')
            localStorage.removeItem('userId')
            localStorage.removeItem('role')
        },
    },
})
