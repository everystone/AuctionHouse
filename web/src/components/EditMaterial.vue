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
          class="form-control"
          placeholder="Price"
          v-model="material.price"
        >
      </div>
      <button class="btn btn-primary" @click="save()">Access</button>
    </div>
  </template>

  <script>
  import auth from '../auth'
  export default {
    data() {
      return {
        material:{
          name: '',
          price: 0
        }
      }
    },
    methods: {
      save() {
        let m = {
          name: this.material.name,
          price: this.material.price
        }

        auth.saveMaterial(this, m)
      }
    },
    route: {
      // Check the users auth status before
      // allowing navigation to the route
      canActivate() {
        return auth.user.authenticated
      }
    }
  }
  </script>