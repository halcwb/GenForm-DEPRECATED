Ext.application({
    name: 'GenForm',

    autoCreateViewport: false,
    appFolder: './Client/GenForm/app',

    controllers: [
        'user.Login',
        'product.Product'
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
        this.getController('product.Product').showProductWindow();
    }
});

