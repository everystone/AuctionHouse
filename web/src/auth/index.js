// src/auth/index.js

import {router} from '../index'

// URL and endpoint constants
const API_URL = 'http://localhost:3333/'
const LOGIN_URL = API_URL + 'users/login/'
const SIGNUP_URL = API_URL + 'users/'
const SAVE_MATERIAL = API_URL + "material/save"
const LIST_MATERIAL = API_URL + "material/list"
export default {

  // User object will let us check authentication status
  user: {
    authenticated: false
  },

  // The object to be passed as a header for authenticated requests
  getAuthHeader() {
    return {
      'Authorization': localStorage.getItem('token')
    }
  },

  saveMaterial(context, material) {
     context.$http.post(SAVE_MATERIAL, material, (data) => {
       
     },{
       // Attach the JWT header
      headers: this.getAuthHeader()
    }).error((err) => {
      context.error = err
    })
  },

  getMaterials(context) {
     context.$http.get(LIST_MATERIAL, (data) => {
       console.log('Updated materials:')
       console.log(data)
       context.materials = data
     },{
       // Attach the JWT header
      headers: this.getAuthHeader()
    }).error((err) => {
      context.error = err
    })
  },

  // Send a request to the login URL and save the returned JWT
  login(context, creds, redirect) {
    context.$http.post(LOGIN_URL, creds, (data) => {
      localStorage.setItem('token', data)
      console.log(data)
      this.user.authenticated = true

      // Redirect to a specified route
      if(redirect) {
        router.go(redirect)        
      }

    }).error((err) => {
      context.error = err
    })
  },

  signup(context, creds, redirect) {
    context.$http.post(SIGNUP_URL, creds, (data) => {
      localStorage.setItem('token', data)

      this.user.authenticated = true

      if(redirect) {
        router.go(redirect)        
      }

    }).error((err) => {
      context.error = err
    })
  },

  // To log out, we just need to remove the token
  logout() {
    localStorage.removeItem('token')
    this.user.authenticated = false
  },

  checkAuth() {
    var jwt = localStorage.getItem('token')
    if(jwt) {
      this.user.authenticated = true
    }
    else {
      this.user.authenticated = false      
    }
  }
}