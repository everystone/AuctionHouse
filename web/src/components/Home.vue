<template>
    <div class="col-sm-6 col-sm-offset-3">
      <h1>Auction House</h1>
      <table class="table">
    <tr v-for="material in materials">
        <td>{{ material.Name }}</td>
        <td>{{ material.Price }}</td>
    </tr>
  </div>
    </div>
  </template>

  <script>
  export default {
    data() {
      return {
        quote: '',
        materials: []
      }
    },
    methods: {
      getQuote() {
        this.$http
          .get('http://localhost:3001/api/random-quote', (data) => {
            this.quote = data;
          })
          .error((err) => console.log(err))
      }
    }
  }
  </script>