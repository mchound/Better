var Collection = function (collection) {
    if (collection === undefined) throw new Error('Collection can not be undefined');
    if (collection.length === undefined) throw new Error('Length must be defined on collection');
    
    var _collection = function (collection) {
        this.length = 0;
        for (var i = 0; i < collection.length; i++) {
            this[i] = collection[i];
            this.length++;
        }
    };

    _collection.prototype.toArray = function () {
        var arr = [];
        for (var i = 0; i < this.length; i++) {
            arr.push(this[i]);
        }
        return arr;
    };

    _collection.prototype.first = function (predicate) {
        for (var i = 0; i < this.length; i++) {
            if (predicate.call(this, this[i], i)) return this[i];
        }
        return undefined;
    };

    _collection.prototype.indexOf = function (predicate) {
        for (var i = 0; i < this.length; i++) {
            if (predicate.constructor === Function && predicate.call(this, this[i], i)) return i;
            else if (predicate === this[i]) return i;
        }
        return -1;
    };

    _collection.prototype.any = function (predicate) {
        for (var i = 0; i < this.length; i++) {
            if (predicate.constructor === Function && predicate.call(this, this[i], i)) return true;
            else if (this[i] === predicate) return true;
        }
        return false;
    };

    _collection.prototype.distinct = function (keySelector) {
        var arr = [];
        var key;
        var comparator = function (item) {
            var _key = keySelector(item);
            return _key === key;
        };

        for (var i = 0; i < this.length; i++) {
            key = keySelector.call(this, this[i], i);
            var exists = new _collection(arr).any(comparator);
            if (exists) continue;
            arr.push(this[i]);
        }
        return arr;
    };

    _collection.prototype.sum = function (selector) {
        var sum = 0;
        for (var i = 0; i < this.length; i++) {
            sum += selector.call(this, this[i], i);
        }
        return sum;
    };

    _collection.prototype.isIn = function (collection, comparator) {
        var arr = this.toArray();
        return arr.filter(function (item) {
            for (var i = 0; i < collection.length; i++) {
                if (comparator.constructor === Function && comparator.call(this, item, collection[i])) {
                    return true;
                }
                else if (item === collection[i]) {
                    return true;
                }
            }
            return false;
        }.bind(this)).length === this.length;
    },

    _collection.prototype.count = function (predicate) {
        var count = 0;
        for (var i = 0; i < this.length; i++) {
            if (predicate.call(this, this[i])) count++;
        }
        return count;
    },

    _collection.prototype.allTrue = function (predicate) {
        for (var i = 0; i < this.length; i++) {
            if (!predicate(this[i])) return false;
        }
        return true;
    },

    _collection.prototype.merge = function (outerArray, keySelector) {
        var innerArr = this.toArray();
        return innerArr.map(function (innerItem) {
            var innerKeyValue = keySelector(innerItem);
            var outerItem = outerArray.filter(function (outerItem) {
                return innerKeyValue === keySelector(outerItem);
            });
            if (outerItem.length === 0) return innerItem;
            else return outerItem[0];
        });
    },

    _collection.prototype.forEachMatch = function (outerArray, keySelector, callback) {
        var innerArr = this.toArray();
        innerArr.forEach(function (innerItem) {
            var innerKeyValue = keySelector(innerItem);
            var outerItem = outerArray.filter(function (outerItem) {
                return innerKeyValue === keySelector(outerItem);
            });
            if (outerItem.length > 0) callback.call(innerArr, innerItem, outerItem[0]);
        });
        return innerArr;
    };

    _collection.prototype.selectMany = function (selector) {
        var arr = [];
        for (var i = 0; i < this.length; i++) {
            arr = arr.concat(selector.call(this, this[i], i));
        }
        return arr;
    };

    _collection.prototype.max = function (selector) {
        var max = -1;
        for (var i = 0; i < this.length; i++) {
            max = Math.max(max, selector.call(this, this[i], i));
        }
        return max;
    };

    return new _collection(collection);
};

module.exports = Collection;