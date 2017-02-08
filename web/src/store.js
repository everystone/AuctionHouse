import Vuex from 'vuex'
import Vue from 'vue'
import Api from './api'
import Constants from './Constants'

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
    FETCH_ITEMS (state, context) {
      Api.get(context, Constants.LIST_MATERIAL).then(list => {
        state.items = list
        state.filtered = list
      }, error => {
        context.error = error
      })
    },
    // SAVE_ITEM (state, context) {
    //   const dto = {
    //     Id: state.selected.id,
    //     Name: state.selected.name,
    //     Price: state.selected.price,
    //     Labor: state.selected.labor,
    //     Produce: state.selected.produce,
    //     craftingRecipe: state.selected.craftingRecipe,
    //     History: state.selected.history
    //   }
    //   Api.post(context, Constants.SAVE_ITEM, dto).then(response => {
    //     // returns list of updated items.
    //     const items = response
    //     console.log(items)
    //     this.updateLocalItems(items)
    //   }, error => {
    //     context.error = error
    //   })
    // },
    SAVE_ITEM (state, data) {
      let context = data.context
      let item = data.item
      return new Promise((resolve, reject) => {
        Api.post(context, Constants.SAVE_ITEM, item).then(items => {
          // VueX Array change detection
          // https://vuejs.org/v2/guide/list.html#Mutation-Methods
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
          resolve(items)
        }, error => {
          reject(error)
        })
      })
    },
    LOGIN (state, data) {
      let context = data.context
      let creds = data.creds
      console.log('creds: ' + creds)
      console.log(Constants.LOGIN_URL)
      Api.post(context, Constants.LOGIN_URL, creds).then(result => {
        Api.user.authenticated = true
        localStorage.setItem('token', result) // eslint-disable-line

        context.$router.push('home')
      }, error => {
        context.error = error
      })
    }
  },
  actions: {
    fetch ({commit}, context) {
      commit('FETCH_ITEMS', context)
    },
    saveEdit ({commit, state}, context) {
      const data = {
        context: context,
        item: {
          Id: state.edit.id,
          Name: state.edit.name,
          Price: state.edit.price,
          Labor: state.edit.labor,
          Produce: state.edit.produce,
          craftingRecipe: state.edit.craftingRecipe,
          History: state.edit.history
        }
      }
      commit('SAVE_ITEM', data)
    },
    quickSave({commit, state}, data) {
      return commit('SAVE_ITEM', data)
    },
    load ({commit}, item) {
      commit('LOAD', item)
    },
    login ({commit}, data) {
      commit('LOGIN', data)
    },
    filter ({commit, state}, str) {
      state.filtered = state.items.filter(m => m.name.toLowerCase().indexOf(str.toLowerCase()) >= 0)
    }
  }
})
