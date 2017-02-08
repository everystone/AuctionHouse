import Vuex from 'vuex'
import Vue from 'vue'
import Api from '../api'
import Constants from '../Constants'

Vue.use(Vuex)
export default new Vuex.Store({
  state: {
    edit: {},
    items: [],
    filtered: []
  },
  mutations: {
    LOAD (state, item) {
      state.edit = item
    },
    RETRIEVED_ITEMS (state, items) {
      state.items = items
      state.filtered = items
    },
      // VueX Array change detection
      // https://vuejs.org/v2/guide/list.html#Mutation-Methods
    REPLACE_ITEMS (state, items) {
      items.forEach(item => {
        let index = state.items.findIndex(m => m.id === item.id)
        if (index >= 0) {
          console.log('Updated item: ' + item.name)
          state.items.splice(index, 1, item)
        } else {
          state.items.push(item)
        }
      })
      state.filtered = state.items // does not trigger update in Home?
    },
    FILTER (state, str) {
      state.filtered = state.items.filter(m => m.name.toLowerCase().indexOf(str.toLowerCase()) >= 0)
    }
  },
  actions: {
    fetch ({commit}, context) {
      // commit('FETCH_ITEMS', context)
      Api.get(Constants.LIST_MATERIAL).then(list => {
        commit('RETRIEVED_ITEMS', list)
      }, error => {
        context.error = error
      })
    },
    saveEdit ({commit, state}, context) {
      let item = {
        Id: state.edit.id,
        Name: state.edit.name,
        Price: state.edit.price,
        Labor: state.edit.labor,
        Produce: state.edit.produce,
        craftingRecipe: state.edit.craftingRecipe,
        History: state.edit.history
      }
      return new Promise((resolve, reject) => {
        Api.post(context, Constants.SAVE_ITEM, item).then(items => {
          commit('UPDATE_ITEMS', items)
          resolve(items)
        }, error => {
          reject(error)
        })
      })
    },
    quickSave({commit, state}, data) {
      return new Promise((resolve, reject) => {
        Api.post(data.context, Constants.SAVE_ITEM, data.item).then(items => {
          commit('UPDATE_ITEMS', items)
          resolve(items)
        }, error => {
          reject(error)
        })
      })
    },
    load ({commit}, item) {
      commit('LOAD', item)
    },
    login ({commit}, data) {
      let context = data.context
      let creds = data.creds
      console.log('creds: ' + creds)
      console.log(Constants.LOGIN_URL)
      Api.post(Constants.LOGIN_URL, creds).then(result => {
        Api.user.authenticated = true
        localStorage.setItem('token', result) // eslint-disable-line
        context.$router.push('home')
      }, error => {
        context.error = error
      })
    },
    filter ({commit, state}, str) {
      commit('FILTER', str)
    }
  }
})
