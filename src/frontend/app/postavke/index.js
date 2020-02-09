import { PostavkeResource } from 'api/resources';

import Postavke from './postavke';
import './chartist'

export default {
	init () {
		return new Promise(resolve => {
      var promise = PostavkeResource().get();

      promise.then(response => {
        Postavke.setPostavke(response.body);
        resolve();
      }, () => {
        console.log("Postavke nisu učitane...");
        resolve();
      });
		});
	}
};
