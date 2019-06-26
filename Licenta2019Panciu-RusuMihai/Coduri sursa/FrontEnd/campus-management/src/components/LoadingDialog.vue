<template>
  <v-dialog v-model="show" hide-overlay persistent width="300">
    <v-card color="light-blue" dark>
      <v-card-text>
        Va rugam sa asteptati...
        <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean
  },
  computed: {
    show: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      }
    }
  }
};
</script>

<style>
</style>
