 <template>
<div class="col-sm-4 col-sm-offset-4">
      <h2>Edit Material</h2>
      <div class="alert alert-danger" v-if="error">
        <p>{{ error }}</p>
      </div>
      <div class="form-group">
        <input
          type="text"
          class="form-control"
          placeholder="Name"
          v-model="material.name"
        >
      </div>
      <div class="form-group">
        <input
          type="number"
          v-model.number="material.price"
          class="form-control"
          placeholder="Price"
        >
      </div>
      <button class="btn btn-primary" @click="save()">Access</button>
    </div>
  </template>

  <script>
  import api from '../api'
  import store from '../store'
  export default {
    data() {
      return {
        material:{
          name: '',
          price: 0,
          labor: 0,
          experience: 0,
          produce: 0,
          id: 0
        }
      }
    },

    methods: {
      save() {
        const mat = {
          Name: this.material.name,
          Price: this.material.price
        }
        api.saveMaterial(this, mat)
      }
    },
    created(){
      this.material = store.state.selected
    },
    route: {
      // Check the users auth status before
      // allowing navigation to the route
      canActivate() {
        return api.user.authenticated
      }
    }
  }
  </script>