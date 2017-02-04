// src/auth/index.js

import {router} from './main'
import store from './store'
// URL and endpoint constants
const API_URL = 'http://192.168.1.6:3333/'
const LOGIN_URL = API_URL + 'users/login/'
const SAVE_MATERIAL = API_URL + 'material/save'
const LIST_MATERIAL = API_URL + 'material/list'
export default {

  // User object will let us check authentication status
  user: {
    authenticated: false
  },

/* eslint-disable no-undef */
  // The object to be passed as a header for authenticated requests
  getAuthHeader() {
    return {
      'Authorization': localStorage.getItem('token')
    }
  },

  saveMaterial(context, material) {
    console.log('saving material: ')
    console.log(material)
    context.$http.post(SAVE_MATERIAL, material,
      {
        headers: this.getAuthHeader()
      }).then(response => {
      }, response => {
        context.error = response.statusText
      })
  },

  getMaterials(context, cb) {
    context.$http.get(LIST_MATERIAL,
      {
        headers: this.getAuthHeader()
      }).then(response => {
        console.log('Updated materials:')
        store.state.list = response.body
        cb()
      }, response => {
        context.error = response.statusText
      })
  },
  navigate(page){
    router.push(page)
  },

  // Send a request to the login URL and save the returned JWT
  login(context, creds, redirect) {
    context.$http.post(LOGIN_URL, creds).then(data => {
      localStorage.setItem('token', data.body)
      this.user.authenticated = true

      // Redirect to a specified route
      if (redirect) {
        router.push(redirect)
      }
    }, (err) => {
      context.error = err.statusText
    })
  },
  logout () {
    localStorage.removeItem('token')
    this.user.authenticated = false
  },

  checkAuth() {
    var jwt = localStorage.getItem('token')
    if (jwt) {
      this.user.authenticated = true
    } else {
      this.user.authenticated = false
    }
  }
}
