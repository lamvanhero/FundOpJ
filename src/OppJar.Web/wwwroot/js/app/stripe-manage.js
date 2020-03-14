var stripeManage = function () {
    var stripe;
    var cardElement;

    var handleServerResponse = function (response) {
        if (response.error) {
            notificationManage.showErrorMessage(result.error.message);
        } else if (response.requires_action) {
            stripe.handleCardAction(
                response.payment_intent_client_secret
            ).then(handleStripeJsResult);
        } else {
            pubsub.publish('PaymentSuccessfully');
        }
    };

    var handleStripeJsResult = function (result) {
        if (result.error) {
            notificationManage.showErrorMessage(result.error.message);
        } else {
            ajaxWrapper.post('/api/stripe/payment/pay', {
                confirmPaymentRequest: {
                    paymentIntentId: result.paymentIntent.id
                }
            }, function (resp) {
                handleServerResponse(resp);
            }, function (error) {
                notificationManage.showErrorMessage(error);
            });
        }
    };

    return {
        init: function (options) {
            stripe = Stripe(options.publicKey);

            var elements = stripe.elements();

            var style = {
                base: {
                    color: "#32325d",
                    fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
                    fontSmoothing: "antialiased",
                    fontSize: "16px",
                    "::placeholder": {
                        color: "#aab7c4"
                    }
                },
                invalid: {
                    color: "#fa755a",
                    iconColor: "#fa755a"
                }
            };

            cardElement = elements.create('card', { style: style });

            cardElement.mount('#card-element');
        },
        checkout: function (billingDetail) {
            stripe.createPaymentMethod({
                type: 'card',
                card: cardElement,
                billing_details: {
                    name: billingDetail.name,
                    email: billingDetail.email
                }
            }).then(function (result) {
                if (result.error) {
                    if (result.error.param === "billing_details[name]") {
                        notificationManage.showErrorMessage("Please fill your name.");
                    }
                } else {
                    ajaxWrapper.post('/api/stripe/payment/pay', {
                        amount: parseFloat(billingDetail.amount),
                        confirmPaymentRequest: {
                            paymentMethodId: result.paymentMethod.id
                        }
                    }, function (resp) {
                        handleServerResponse(resp);
                    }, function (error) {
                        notificationManage.showErrorMessage(error);
                    });
                }
            });
        },
        saveCard: function (data) {
            stripe.createPaymentMethod({
                type: 'card',
                card: cardElement,
                billing_details: {
                    name: data.cardholderName
                }
            }).then(function (result) {
                if (result.error) {
                    notificationManage.showErrorMessage(result.error.message);
                } else {
                    ajaxWrapper.post('/api/stripe/card/save-card', {
                        customerId: data.customerId,
                        paymentMethodId: result.paymentMethod.id
                    }, function (resp, stat) {
                        notificationManage.showSuccessMessage("Saved.");
                    }, function (error) {
                        notificationManage.showErrorMessage(error);
                    });
                }
            });
        },
        pay: function (data) {
            ajaxWrapper.post('/api/stripe/payment/pay', {
                amount: data.amount,
                confirmPaymentRequest: {
                    paymentMethodId: data.paymentMethodId,
                    customerId: data.customerId
                }
            }, function (resp) {
                handleServerResponse(resp);
            }, function (error) {
                notificationManage.showErrorMessage(error);
            });
        }
    };
}();