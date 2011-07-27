Ext.define('GenForm.view.product.PackageWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.packagewindow',

    width: 500,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = me.createPackageForm();

        me.callParent(arguments);
    },

    createPackageForm: function () {
        var me = this;
        return me.createForm({ xtype: 'widget.packageform', name: 'PackageForm' });
    },

    loadWithPackage: function (productPackage) {
        var me = this;
        me.forms.PackageForm.loadRecord(productPackage);
    }

});