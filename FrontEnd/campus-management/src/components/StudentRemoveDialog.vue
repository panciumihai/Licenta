<template>
  <v-dialog v-model="show" max-width="400">
    <v-card>
      <v-card-title class="headline">{{ "Decazare " + selectedStudent.fullName}}</v-card-title>

      <v-card-text v-if="selectedStudent.gender == 'M'">
        {{"Confirmă decazarea din "+selectedStudent.hostelName +" a studentului "+ selectedStudent.fullName + " din anul " + selectedStudent.year + " cu punctajele " + selectedStudent.score +
        " și "+selectedStudent.secondScore}}
      </v-card-text>
      <v-card-text v-else>
        {{"Confirmă decazarea din "+selectedStudent.hostelName +" a studentei "+ selectedStudent.fullName + " din anul " + selectedStudent.year + " cu punctajele " + selectedStudent.score +
        " și "+selectedStudent.secondScore}}
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn color="grey darken-3" flat="flat" @click="removeConfirmation(false)">Anuleaza</v-btn>

        <v-btn color="blue darken-1" flat="flat" @click="removeConfirmation(true)">Confirmă</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean,
    selectedStudent: {}
  },
  methods: {
    removeConfirmation(valid) {
      let studentConfirmation = {
        id: this.selectedStudent.id,
        confirmed: false,
        studentsGroupId: this.selectedStudent.studentsGroupId
      };
      if (valid)
        this.$store
          .dispatch("addStudentConfirmation", studentConfirmation)
          .then(result => {
            let snackbar = {
              text: "Decazarea a fost efectuata cu succes!",
              color: "success"
            };
            console.log(result);
            this.$store.dispatch("getConfirmedStudents");
            this.$store.dispatch("showSnackbar", snackbar);
          })
          .catch(error => {
            let snackbar = {
              text: "Hopa! Decazarea nu a putut fi efectuata!",
              color: "error"
            };
            this.$store.dispatch("showSnackbar", snackbar);
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
