<template>
    <transition name="modal">
        <div class="modal-mask">
            <div class="modal-wrapper">
                <div class="modal-container form-inline">
                <h2>{{material.name}}</h2>
                <chart :height="200" v-bind:chartData="chartData"></chart>
                    <div class="form-group">
                      <label>Price</label>  
                      <input type="number" v-model.number="newPrice" autofocus>
                    </div>
                    <div class="form-group">
                    <button class="btn btn-primary" @click="save">Update</button>
                    </div>
                    <div class="form-group">
                    <button class="btn btn-error" @click="$emit('close')">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </transition>
</template>


<script>
  import Chart from './HistoryChart'
  export default {
    props: ['material'],
    data() {
      return {
        newPrice: this.material.price,
        chartData: []
      }
    },
    components: {
      'chart': Chart
    },
    methods: {
      save() {
        console.log(this.material)
        // Only send minimum, rest is done on backend.
        const updated = {
          id: this.material.id,
          price: this.newPrice
        }
        // api.saveMaterial(this, updated, () => {
        //   this.$emit('close')
        // })
        this.$store.dispatch('quickSave', {context: this, item: updated}).then(res => {
          this.$emit('close')
        }, error => {
          console.log('Error: ' + error)
        })
      }
    },
    created() {
      if (this.material.history) {
        this.chartData = {
          labels: this.material.history.map(h => new Date(h.date).toLocaleDateString()),
          datasets: [
            {
              label: this.material.name,
              backgroundColor: '#f87979',
              data: this.material.history.map(h => h.price)
            }
          ]
        }
      }
    }
  }

</script>