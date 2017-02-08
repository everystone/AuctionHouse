import Vue from 'vue'
import App from './components/App.vue'
import Home from './components/Home.vue'
import Edit from './components/Edit.vue'
// import Signup from './components/Signup.vue'
import Login from './components/Login.vue'
import auth from './api'
import VueRouter from 'vue-router'
import VueResource from 'vue-resource'
import store from './state/store'
Vue.use(VueResource)
Vue.use(VueRouter)

// https://vuejs.org/v2/guide/migration-vue-router.html
// This is the new way of doing it.

const router = new VueRouter({
  routes: [
    { path: '/home', component: Home },
    { path: '/edit', component: Edit },
    { path: '/login', component: Login },
    { path: '*', redirect: '/login' }
  ]
})

// Navigation guards: https://router.vuejs.org/en/advanced/navigation-guards.html#perroute-guard
router.beforeEach((to, from, next) => {
  // Allways allow login
  if (to.path === '/login') {
    next()
  }
  next(auth.user.authenticated)
})
/* eslint-disable no-new */

new Vue({
  el: '#app',
  router: router,
  store: store,
  render: h => h(App)
})

export { router }
