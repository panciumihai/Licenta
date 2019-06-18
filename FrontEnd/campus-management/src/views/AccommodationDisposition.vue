<template>
  <v-container>
    <v-card>
      <v-card-title>
        <div class="display-1 mx-3">Dispozitie de cazare</div>
      </v-card-title>
      <v-card-text v-if="student.studentsGroupId != null">
        <v-layout row wrap>
          <v-flex class="ma-3">
            <div class="headline">
              Felicitari,
              <span>{{student.lastName +" "+student.firstName }}</span>!
            </div>
          </v-flex>
          <v-flex class="ma-3">
            <div class="headline">
              Ai fost repartizat in caminul
              <span class="blue--text">{{student.hostelName}}</span>. Pentru a ocupa locul trebuie sa descarci dispozitia de cazare de mai jos si sa te prezenti cu ea tiparita la administratorul de cazari.
              <span
                class="headline green--text"
              >Succes!</span>
            </div>
          </v-flex>
          <v-flex class="mt-4">
            <v-layout justify-center>
              <v-btn dark color="blue" @click="generateDisposition()">Descarca dispozitia de cazare</v-btn>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-card-text>
      <v-card-text v-else>
        <v-layout row wrap>
          <v-flex xs12 md12>
            <v-layout justify-center>
              <div class="headline ma-3 red--text">Nu aveti nicio displozitie de cazare.</div>
            </v-layout>
          </v-flex>
          <v-flex xs12 md12>
            <v-layout justify-center>
              <div
                class="headline"
              >Daca considerati asta ca fiind o greseala, contactati administratia.</div>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import jsPDF from "jspdf";

export default {
  methods: {
    getAccommodationYear() {
      var today = new Date();

      if (today.getMonth() >= 5) {
        let nyear = today.getFullYear() + 1;
        return today.getFullYear() + "-" + nyear;
      } else {
        let pyear = today.getFullYear() - 1;
        return pyear + "-" + today.getFullYear();
      }
    },
    getTodayDate() {
      var today = new Date();
      //  let month =
      return (
        String(today.getDate()).padStart(2, "0") +
        "." +
        String(today.getMonth() + 1).padStart(2, "0") +
        "." +
        today.getFullYear()
      );
    },
    generateDisposition() {
      var doc = new jsPDF();
      doc.setFontSize(16);
      doc.setFont("times");
      doc.setFontStyle("normal");
      doc.text(
        "Dispozitia de cazare nr. ........ din data " + this.getTodayDate(),
        105,
        20,
        null,
        null,
        "center"
      );
      doc.text(
        "Pentru anul universitar " + this.getAccommodationYear(),
        105,
        30,
        null,
        null,
        "center"
      );

      doc.text(
        "Se acorda dreptul de cazare studentului " +
          this.student.lastName.toUpperCase() +
          " " +
          this.student.firstName.toUpperCase() +
          ",",
        25,
        60,
        null,
        null,
        "left"
      );
      doc.text(
        "din anul " +
          this.student.year +
          ", de la Universitatea ALEXANDRU IOAN CUZA, specializarea ",
        20,
        70,
        null,
        null,
        "left"
      );
      doc.text(
        "INFORMATICA, in caminul " +
          this.student.hostelName +
          ", camera ............., CNP " +
          this.student.cnp,
        20,
        80,
        null,
        null,
        "left"
      );

      doc.text("Decan,", 20, 130);
      doc.text("Conf. dr. Adrian Iftene", 20, 140);

      doc.text("Administrator sef,", 200, 130, null, null, "right");
      doc.text("Negrescu Radu", 200, 140, null, null, "right");

      doc.text("Semnatura beneficiarului,", 200, 180, null, null, "right");
      doc.text("___________________", 200, 200, null, null, "right");

      doc.save("Dispozitie de cazare.pdf");
    }
  },
  computed: {
    student() {
      return this.$store.getters.student;
    }
  },
  mounted() {
    //this.generateDisposition();
    this.$store.dispatch("getStudentWithHostel");
  }
};
</script>

<style>
</style>
