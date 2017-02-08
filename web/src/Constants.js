let API_URL = 'http://' + window.location.hostname + ':3333/'
export default {
  LOGIN_URL: API_URL + 'users/login/',
  SAVE_ITEM: API_URL + 'material/save',
  LIST_MATERIAL: API_URL + 'material/list'
}
