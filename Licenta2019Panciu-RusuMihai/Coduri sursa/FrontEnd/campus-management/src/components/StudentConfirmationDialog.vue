<template>
  <v-dialog v-model="show" max-width="400">
    <v-card>
      <v-card-title class="headline">{{ "Confirmare " + selectedStudent.fullName}}</v-card-title>

      <v-card-text>
        {{"Confirmă locul în "+selectedStudent.hostelName +" deținut de "+ selectedStudent.fullName + " din anul " + selectedStudent.year + " cu punctajele " + selectedStudent.score +
        " și "+selectedStudent.secondScore}}
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn color="grey darken-3" flat="flat" @click="acceptConfirmation(false)">Anuleaza</v-btn>

        <v-btn color="blue darken-1" flat="flat" @click="acceptConfirmation(true)">Confirmă</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
/* eslint-disable */

export default {
  props: {
    selectedStudent: {},
    value: Boolean
  },
  methods: {
    acceptConfirmation(valid) {
      let studentConfirmation = {
        id: this.selectedStudent.id,
        confirmed: true,
        studentsGroupId: this.selectedStudent.studentsGroupId
      };
      if (valid)
        this.$store
          .dispatch("addStudentConfirmation", studentConfirmation)
          .then(result => {
            let snackbar = {
              text: "Confirmare adaugata cu succes!",
              color: "success"
            };
            console.log(result);
            this.$store.dispatch("showSnackbar", snackbar);
            this.$store.dispatch("getHostelsStatusAllocationPreview");
          })
          .catch(error => {
            let snackbar = {
              text: "Hopa! Confirmarea nu a putut fi adaugata!",
              color: "error"
            };
            this.$store.dispatch("showSnackbar", snackbar);
            console.log(error);
          });
      this.show = false;
      //this.selectedStudent = {};
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
