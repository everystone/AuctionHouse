<template>
  <div class="col-sm-4 col-sm-offset-4">
    <h2>{{ material.name }}</h2>
    <div class="alert alert-danger" v-if="error">
      <p>{{ error }}</p>
    </div>
    <div class="form-group">
      <label for="name">Name</label>
      <input id="name" type="text" class="form-control" placeholder="Name" v-model="material.name">
    </div>
    <div class="form-group">
      <label for="price">Price</label>
      <input id="price" type="number" v-model.number="material.price" class="form-control" placeholder="Price">
      </div>
      <div class="form-group">
      <label for="labor">Labor</label>
      <input id="labor" type="number" v-model.number="material.labor" class="form-control" placeholder="Labor">
      </div>
      <div class="form-group">
      <label>Produce</label>
      <input type="number" v-model.number="material.produce" class="form-control" placeholder="Produce">
      </div>

    <div class="form-inline">
     <div class="form-group">
      <select v-model="selected" class="form-control">
        <option v-for="material in mats" v-bind:value="material.id">
        {{ material.name}}
        </option>
        </select>
     </div>
     <div class="form-group">
        <input type="number" v-model.number="count" class="form-control" placeholder="Count">
     </div>
     <div class="form-group">
        <button class="btn btn-primary" @click="add()">Add</button>
     </div>
     </div>

    <table class="table">
      <thead>
        <tr>
          <th>Material</th>
          <th>Count</th>
        </tr>
      </thead>
      <tbody>
      <tr v-for="info in recipeData">
        <td>{{ info.name }}</td>
        <td>{{ info.count }}</td>
      </tr>
      </tbody>
    </table>

    <button class="btn btn-primary" @click="save()">Save</button>
  </div>
</template>

  <script>
  export default {
    data() {
      return {
        selected: {},
        count: 1,
        error: '',
        recipeData: []
      }
    },
    computed: {
      material: function() {
        return this.$store.state.edit
      },
      mats: function() {
        return this.$store.state.items
      }
    },
    methods: {
      save() {
        this.$store.dispatch('saveEdit', this)
        this.$router.push('home')
      },
      generateRecipe() {
        if (!this.material.craftingRecipe) {
          return
        }
        this.recipeData = this.material.craftingRecipe.map(m => {
          return {
            name: this.$store.state.items.find(i => i.id === m.id).name,
            count: m.count
          }
        })
      },
      add(){
        if (this.material.craftingRecipe == null) {
          this.material.craftingRecipe = []
        }
        this.material.craftingRecipe.push({
          'id': this.selected,
          'count': this.count
        })
        this.generateRecipe()
        console.log(this.material)
      }
    },
    created(){
      console.log('Loaded:')
      this.generateRecipe()
      console.log(this.material)
    }
  }
  </script>