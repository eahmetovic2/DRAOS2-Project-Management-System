var evaluateAll = (vm, resolve) => {
  var keys = Object.keys(resolve);

  return keys.reduce((result, key) => {
    var func = resolve[key];
    if (typeof func === "function")
      result[key] = func.apply(vm);
    return result;
  }, {});
};

var processResult = result => {
  // hack, c'est la js
  if (result && result.status && result.headers) {
    if (result.ok)
      return result.body;
    return null;
  }

  return result;
};

var promiseAll = promises => {
  var values = Object.values(promises);
  var keys = Object.keys(promises);

  return Promise.all(values).then(results => {
    return keys.reduce((result, key, index) => {
      result[key] = processResult(results[index]);
      return result;
    }, {});
  });
};

var doResolve = vm => {
  if (!vm.$options.resolve)
    return;

  var promises = evaluateAll(vm, vm.$options.resolve);
  promiseAll(promises)
    .then((result) => {
      vm.$set(vm, 'resolved', result);
    });
};

export default {
  beforeRouteEnter (to, from, next) {
    next(doResolve);
  },

  beforeRouteUpdate (to, from, next) {
    if (this.$options.resolve)
      this.$set(this, 'resolved', null);

    next();
    doResolve(this);
  }
};
