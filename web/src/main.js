import Vue from 'vue'
import App from './components/App.vue'
import Home from './components/Home.vue'
import Edit from './components/Edit.vue'
// import Signup from './components/Signup.vue'
import Login from './components/Login.vue'
import VueRouter from 'vue-router'
import VueResource from 'vue-resource'
Vue.use(VueResource)
Vue.use(VueRouter)

// https://vuejs.org/v2/guide/migration-vue-router.html
// This is the new way of doing it.

export var router = new VueRouter({
  routes: [
    { path: '/home', component: Home },
    { path: '/material', component: Edit },
    { path: '/login', component: Login },
    { path: '*', redirect: '/login' }
  ]
})

/* eslint-disable no-new */

new Vue({
  el: '#app',
  router: router,
  render: h => h(App)
})
