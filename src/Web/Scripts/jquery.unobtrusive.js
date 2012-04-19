(function ($) {
    var $jQVal = $.validator || {},
        $jQValUn = $jQVal.unobtrusive;

    if ($jQValUn) {
        $(':input[data-val=true]').livequery(function () {
            var form = $(this).closest('form');
            $jQValUn.parse(form);
            var validator = form.data('validator');
            var unobtrusiveValidation = form.data('unobtrusiveValidation');
            if (validator && unobtrusiveValidation)
                $.extend(validator.settings, unobtrusiveValidation.options);
        });
    }
})(jQuery);