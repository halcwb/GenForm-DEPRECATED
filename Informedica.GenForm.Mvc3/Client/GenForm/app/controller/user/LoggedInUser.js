/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 20:17
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.controller.user.LoggedInUser', {
    extend: Object,
    singleton: true,

    config: {
        name: 'unknown',
        loggedIn: false
    },

    constructor: function(config) {
        if (!config) Ext.MessageBox.alert('Config not defined');

        this.initConfig(config);

        return this;
    }
});