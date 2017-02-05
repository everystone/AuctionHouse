
export default {

  debug: true,
  state: {
    selected: {},
    list: []
  },
  setList(list) {
    this.state.list = list
  },
  setSelected (selected) {
    // this.debug && console.log('setMessageAction triggered with', newValue)
    this.state.selected = selected
  },
  clearSelected () {
   // this.debug && console.log('clearMessageAction triggered')
    this.state.selected = {}
  },
  fetchList(){
  }
}
