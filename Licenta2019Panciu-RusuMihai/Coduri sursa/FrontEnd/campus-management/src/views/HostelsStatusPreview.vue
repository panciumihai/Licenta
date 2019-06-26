<template>
  <v-container>
    <v-layout row wrap align-center justify-center>
      <SpecificHostelStatus
        class="mb-3"
        v-for="hostelStatus in hostelsStatus"
        :key="hostelStatus.id"
        :hostelStatus="hostelStatus"
      ></SpecificHostelStatus>
      <v-btn
        dark
        color="blue"
        @click="addHostelsStatus(); snackbar.active = true"
      >Salveaza repartizari</v-btn>
    </v-layout>
    <v-snackbar
      v-model="snackbar.active"
      bottom
      right
      :timeout="snackbar.timeout"
      :vertical="snackbar.mode === 'vertical'"
      color="success"
    >
      {{ snackbar.text }}
      <v-btn color="white" flat @click="snackbar.active = false">Close</v-btn>
    </v-snackbar>
  </v-container>
</template>

<script>
import SpecificHostelStatus from "@/components/SpecificHostelStatus";

export default {
  components: {
    SpecificHostelStatus
  },
  data() {
    return {
      snackbar: {
        active: false,
        y: "top",
        x: null,
        mode: "",
        timeout: 6000,
        text: "Locurile au fost salvate cu succes!"
      }
    };
  },
  methods: {
    addHostelsStatus() {
      this.$store.dispatch("addOrUpdateHostelsStatus", this.hostelsStatus);
    }
  },
  computed: {
    hostelsStatus() {
      return this.$store.getters.hostelsStatus;
    }
  },
  mounted() {
    this.$store.dispatch("getHostelsStatusPreview");
  }
};
</script>

<style>
</style>
