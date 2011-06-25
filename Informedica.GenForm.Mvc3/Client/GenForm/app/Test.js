/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 11:45 AM
 * To change this template use File | Settings | File Templates.
 */

Ext.application({
    name: 'GenForm',

    autoCreateViewport: false,
    appFolder: '../Client/GenForm/app',

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


describe('Login', function () {
    it('The user should see a login window at start up', function () {
         
    });
});