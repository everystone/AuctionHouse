<template>
    <transition name="modal">
        <div class="modal-mask">
            <div class="modal-wrapper">
                <div class="modal-container">
                <h2>{{material.name}}</h2>
                    <div class="form-group">
                        <input type="text">
                    </div>
                    <div class="form-group">
                    <button class="button button-primary">Update</button>
                    </div>
                </div>
            </div>
        </div>
    </transition>
</template>


<script>
  export default {
    data() {
      return {
        material: {}
      }
    }
  }

</script>