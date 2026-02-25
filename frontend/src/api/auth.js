import http from "./http";

export const login = (userName, password) => {
    return http.post('/Auth/login', {userName, password})
}