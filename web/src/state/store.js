import Vuex from 'vuex'
import Vue from 'vue'
import Actions from './actions'

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
          // Add custom class property, to highlight item in table.
          item.highlight = true
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
  actions: Actions
})
