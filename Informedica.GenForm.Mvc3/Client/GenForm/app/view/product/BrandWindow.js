Ext.define('GenForm.view.product.BrandWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.brandwindow',

    mixins: ['GenForm.lib.util.mixin.FormCreator'],

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = this.createBrandForm();

        me.callParent(arguments);
    },

    createBrandForm: function () {
        var me = this;
        return me.createForm({ xtype: 'widget.brandform', name: 'BrandForm'});
    },

    loadWithBrand: function (brand) {
        var me = this;
        me.forms.BrandForm.loadRecord(brand);
    }

});