/**
 * Created by .
 * User: hal
 * Date: 7-5-11
 * Time: 6:11
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.PackageName', {
    extend: 'Ext.data.Model',
    alias: 'widget.packagenamemodel',

    fields: [ {name: 'PackageName', type: 'string' }],

    proxy: {
        type: 'direct',
        directFn: Product.GetPackageNames
    }
});