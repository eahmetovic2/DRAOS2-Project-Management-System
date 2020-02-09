<template>
<v-app id="prijava-cont">
    <v-layout row class="text-xs-center flex-0">
        <v-flex id="prijava">
            <!-- <img src="vuetifylogo.png"> -->
        </v-flex>
    </v-layout>

    <v-layout justify-center wrap class="prijava-form" style="margin-top: 220px">
      <v-flex xs12 md8 lg4>
        <material-card
          color="primary"
          title="Prijava"
          text="Unesite podatke za prijavu"
        >
          <v-form v-model="valid" ref="form">
            <v-container py-0>
              <v-layout column wrap>
                <v-flex xs12 md4>
                    <v-text-field :autofocus="true" placeholder=" " id="korisnicko-ime" @keyup.enter="provjeriLozinku()" label="Korisničko ime" :rules="korisnickoImeRules" v-model="korisnickoIme" required class="required"></v-text-field>
                </v-flex>
                <v-flex xs12 md4>
                    <v-text-field placeholder=" " id="lozinka" @keyup.enter="provjeriLozinku()" label="Lozinka" :append-icon="e1 ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (e1 = !e1)" :type="e1 ? 'password' : 'text'" :rules="lozinkaRules" v-model="lozinka" required class="required"></v-text-field>
                </v-flex>
                <v-flex xs12 text-xs-right>
                  <v-btn type="button" id="prijava-btn" @click="provjeriLozinku()" color="primary">Prijava</v-btn>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </material-card>
      </v-flex>
    </v-layout>

     
</v-app>
</template>
<script>
import Auth from 'auth';
import Identity from 'auth/identity';
import {TokenResource} from 'api/resources'

export default {
    name: 'Prijava',
    data() {
        return {
            valid: false,
            e1: true,
            korisnickoIme: '',
            odabranaUloga: null,
            uloge: null,
            korisnickoImeRules: [
                (v) => !!v || 'Korisničko ime je obavezno'
            ],
            lozinkaRules: [
                (v) => !!v || 'Lozinka je obavezna'
            ],
            lozinka: ''
        };
    },

    methods: {
        provjeriLozinku() {
            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);
            var that = this;

            function success(model) {
                console.log(model);
                var uloge = model.body.items;
                if (uloge.length === 1) {
                    that.onSubmit(uloge[0].id);
                } else {
                    that.uloge = uloge;
                    that.odabranaUloga = uloge[0].id;
                }
            }
            
            function error() {
                that.$toast.error("Pogrešno korisničko ime ili lozinka. Pokušajte ponovo...");
            }

            if (that.valid) {
                TokenResource().provjeriLozinku({
                    korisnickoIme: that.korisnickoIme
                }, {
                    lozinka: that.lozinka
                }).then(success, error);
            }
        },
        onSubmit(ulogaId) {
            var that = this;
            var success = function () {
                console.log("RADI")
                // Ako se koristi vue-router onda ne radi meni u topbar
                var token = Identity.getToken();       
                if(token.vlasnik.jezik)  
                {
                 that.$store.commit('tmsVuex/setOdabraniJezik', token.vlasnik.jezik);
    
                }
                 window.location.href = '/';
            };
            var error = function () {
                that.$toast.error("Pogrešno korisničko ime ili lozinka. Pokušajte ponovo...");
            };

            Auth.signIn(that.korisnickoIme, that.lozinka, ulogaId)
                .then(success, error);        
        }
    },

    beforeMount() {
        if (Auth.isAuthenticated()) {
            this.$router.push({
                name: 'home.dashboard'
            });
        }
    }
};
</script>

<style>
#prijava-cont {
    display: flex;
    flex-direction: column;
}

#prijava-cont .application--wrap {
    background-repeat: no-repeat;
    background-size: cover, contain;
    padding: 20px;
}

.flex-0 {
    flex: 0;
}

#prijava-layout {
    position: relative;
    top: -60px;
    justify-content: center;
    flex: 1;
    color: rgba(255, 255, 255, 0.8);
}


#prijava img {
    height: 100px;
}



@media (max-height: 500px) {
    #prijava-layout {
        top: 0;
    }
}

.prijava-form {
    margin-top: 50px;
}

#account-menu .btn,
.menu__content .icon,
.menu__content .list__tile__title {
    font-size: 14px;
}

#prijava-text {
    color: #383838 !important;
    font-size: 12px;
}
</style>
