<template>
  <v-dialog v-model="show" max-width="400px">
    <v-card class="pr-0">
      <v-date-picker v-model="picker" :first-day-of-week="1" full-width landscape locale="ro-ro"></v-date-picker>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean
  },
  data() {
    return {
      picker: new Date().toISOString().substr(0, 10)
    };
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
