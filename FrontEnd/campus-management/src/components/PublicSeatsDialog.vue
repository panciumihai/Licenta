<template>
  <v-dialog v-model="show" max-width="400">
    <v-card>
      <v-card-title class="headline">{{ "Publicarea turului actual" }}</v-card-title>

      <v-card-text>{{"Doriti sa afisati public lista cu studenti din turul actual? Repartizarile afisate sunt pentru cererile create incepand cu " + stage.startDate + " pana la " + stage.endDate + " inclusiv."}}</v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn color="grey darken-3" flat="flat" @click="publishSeats(false)">Anuleaza</v-btn>

        <v-btn color="blue darken-1" flat="flat" @click="publishSeats(true)">Confirmă</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean,
    stage: {},
    studentsAllocations: {}
  },
  methods: {
    publishSeats(decision) {
      if (decision)
        this.$store
          .dispatch("addStudentsAllocations", this.studentsAllocations)
          .then(response => {
            this.$store.dispatch("showSnackbar", {
              text: "Turul a fost publicat cu succes!",
              color: "success"
            });
            console.log(response);
          })
          .catch(error => {
            this.$store.dispatch("showSnackbar", {
              text: "Hopa! Turul nu a putut fi publicat!",
              color: "error"
            });
            console.log(error);
          });
      this.show = false;
    }
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
