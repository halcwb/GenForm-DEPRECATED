Ext.application({
    name: 'GenForm',

    autoCreateViewport: false,
    appFolder: './Client/GenForm/app',

    controllers: [
        'GenForm.controller.user.LoginController',
        'GenForm.controller.product.ProductController'
    ],

    launch: function() {
        GenForm.application = this;

        Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });

        this.createLoginWindow().show();
    },

    createLoginWindow: function () {
        return Ext.create('GenForm.view.user.LoginWindow');
    },

    showProductWindow: function () {
        this.createProductWindow().show();
    },

    createProductWindow: function () {
        return Ext.create('GenForm.view.product.ProductWindow');
    }
});

