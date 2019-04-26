dotnetHelper = function () {
    var subscriptions = [];
    var latestSubscriptionId = 0;

    function forwardEventToDotnet(subscription, eventData) {
        var filteredData = subscription.filter ? subscription.filter(eventData) : eventData;

        subscription.dotnetRef.invokeMethodAsync(subscription.dotnetMethod, filteredData);
    }

    function subscribe(dotnetRef, dotnetMethod, subscribe, unsubscribe, eventDataFilter) {
        var subscriptionId = ++latestSubscriptionId;

        var attachTo = eval(subscribe);
        if (attachTo === undefined) {
            throw Error('attachTo is undefined');
        }

        var subscription = {
            id: subscriptionId,
            unsubscribe: unsubscribe ? eval(unsubscribe) : unsubscribe,
            dotnetRef,
            dotnetMethod,
            filter: eventDataFilter ? eval(eventDataFilter) : eventDataFilter
        };

        var handler = (e) => {
            forwardEventToDotnet(subscription, e);
        }

        subscription.subscribe = eval(subscribe)(handler);

        subscriptions.push(subscription);

        return subscriptionId;
    }

    function unsubscribe(subscriptionId) {
        var subscription = subscriptions.find(s => s.id === subscriptionId);

        if (subscription.unsubscribe) {
            subscription.unsubscribe(subscription.handler);
        }

        subscriptions = subscriptions.filter(s => s.id !== subscriptionId);
    }

    return {
        subscribe,
        unsubscribe
    }
}();