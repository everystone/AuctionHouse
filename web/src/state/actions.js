import Api from '../api'
import Constants from '../Constants'

export default {
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
      Api.post(Constants.SAVE_ITEM, item).then(items => {
        commit('REPLACE_ITEMS', items)
        resolve(items)
      }, error => {
        reject(error)
      })
    })
  },
  quickSave({commit, state}, data) {
    return new Promise((resolve, reject) => {
      Api.post(Constants.SAVE_ITEM, data.item).then(items => {
        commit('REPLACE_ITEMS', items)
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
