/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 7:22 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.app.config = {
    name: 'GenForm',

    autoCreateViewport: false,

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
};