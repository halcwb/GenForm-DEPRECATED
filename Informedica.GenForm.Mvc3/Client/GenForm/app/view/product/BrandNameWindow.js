Ext.define('GenForm.view.product.BrandNameWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.brandnamewindow',

    mixins: ['GenForm.lib.util.mixin.FormCreator'],

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = this.createBrandNameForm();

        me.callParent(arguments);
    },

    createBrandNameForm: function () {
        var me = this;
        return me.createForm({ xtype: 'brandnameform', name: 'BrandNameForm'});
    },

    loadWithBrand: function (brand) {
        var me = this;
        me.forms.BrandNameForm.loadRecord(brand);
    }

});