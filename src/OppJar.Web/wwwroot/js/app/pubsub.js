var pubsub = {
    queue: {},
    subscribe: function(eventName, fn) {
        this.queue[eventName] = this.queue[eventName] || [];
        this.queue[eventName].push(fn);
    },
    unSubscribe: function(eventName, fn) {
        if(this.queue[eventName]) {
            for(var i = 0; i < this.queue[eventName].length; i++) {
                if(this.queue[eventName][i] === fn){
                    this.queue[eventName].splice(i, 1);
                    break;
                }
            }
        }
    },
    publish: function(eventName, args){
        if(this.queue[eventName]){
            this.queue[eventName].forEach(function(fn){
                fn(args);
            });
        }
    }
};