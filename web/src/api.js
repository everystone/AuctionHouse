
import Vue from 'vue'
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

  post(url, data) {
    return new Promise((resolve, reject) => {
      Vue.http.post(url, data, {
        headers: this.getAuthHeader()
      }).then(response => {
        resolve(response.body)
      }, response => {
        reject(response.body)
      })
    })
  },
  get(url) {
    return new Promise((resolve, reject) => {
      Vue.http.get(url, {
        headers: this.getAuthHeader()
      }).then(response => {
        resolve(response.body)
      }, response => {
        reject(response.body)
      })
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
